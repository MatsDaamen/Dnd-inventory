<script lang="ts">
    import type { PageData } from './$types';
    import {
		Button,
		Modal,
        Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell,
        Toast,
        TabItem,
        Tabs
	} from 'flowbite-svelte';
    import { PlusSolid, TrashBinSolid, ClipboardCheckSolid, InfoCircleSolid, ArrowLeftSolid, CheckCircleSolid } from 'flowbite-svelte-icons';
    import type { Session, joinKey } from '$lib/API/sessions';
    import { fly } from 'svelte/transition';
    import InventoryTable from '$lib/Components/InventoryTable.svelte';
    import type { inventory } from '$lib/API/inventory';
    import { redirect } from '@sveltejs/kit';
    import { onMount } from 'svelte';
    import * as signalr from '@microsoft/signalr';
    import { getUserName } from '$lib/API/auth';
    

    export let data: PageData;
    const session: Session = data.session;
    const userId: string = data.userid;
    let inventories: inventory[] = data.inventory;

    let hubconnection: signalr.HubConnection;

    let selectedJoinCode: joinKey | null = null;

    let selectedUserId: string | null = null;

    let showJoinKeyModel = false;

    let showToast = false;

    function copyJoinCode(joinCode: string){
        navigator.clipboard.writeText(joinCode);
        showToast = true;
    }

    if(session.users.find(x => x.userId == userId) == undefined)
        throw redirect(302, '/sessions');

    onMount(async () => {
        hubconnection = new signalr.HubConnectionBuilder()
        .withUrl(import.meta.env.VITE_HUB_URL + "/inventory")
        .withAutomaticReconnect()
        .build();

        await hubconnection.start();

        await hubconnection.invoke("OnConnection", userId, hubconnection.connectionId);

        hubconnection.on("sendUpdateInventory", async (inventory : string) => {

            let newInventories : inventory[] = JSON.parse(inventory);

            if(newInventories.some(x => inventories.some(y => y.userId == x.userId))){
                if(session.createdBy != userId)
                    newInventories = newInventories.splice(0, newInventories.length, newInventories.find(inv => inv.userId == userId))

                for (let i = 0; i < newInventories.length; i++) {
                    newInventories[i].ownerName = await getUserName(newInventories[i].userId);
                }
            
                inventories = newInventories;
            }
        });
	});
</script>

<div class="grid grid-rows-[max-content,1fr] text-xs md:text-base lg:text-lg p-4 md:p-12 gap-4">

	<div class="flex flex-col md:flex-row gap-4 md:gap-0 md:justify-between">
        <div class="block">
            <Button href="/sessions">
                <ArrowLeftSolid/>
            </Button>
		</div>
		<div class="block">
			<h1 class="text-lg md:text-2xl font-bold">{session.name}</h1>
		</div>
    </div>
    <Tabs style="underline">
        <TabItem open={session.createdBy !== userId} title="inventories">
            <div class="flex flex-col md:flex-row gap-2 md:gap-0 md:justify-between">
                {#if session.createdBy === userId}
                    <div class="block">
                        {#each inventories as inventory}
                            <InventoryTable inventory={inventory} sessionUsers={session.users} />
                        {/each}
                    </div>
                {:else}
                    <div class="block">
                        <InventoryTable inventory={inventories.find(inv => inv.userId == userId)} sessionUsers={session.users} />
                    </div>
                {/if}
                <div class="block">
                </div>
            </div>
        </TabItem>
        {#if session.createdBy === userId}
        <TabItem open={session.createdBy === userId} title="members and invite code">
            <div class="flex flex-col md:flex-row gap-2 md:gap-0 md:justify-between">
                <div class="block">
                    <Table>
                        <TableHead class="text-white dark:text-white bg-primary-500">
                            <TableHeadCell>member name</TableHeadCell>
                            <TableHeadCell>sessionOwner</TableHeadCell>
                            <TableHeadCell>actions</TableHeadCell>
                        </TableHead>
                        <TableBody>
                            {#each session.users as user}
                            <TableBodyRow class="justify-center items-center">
                                <TableBodyCell>{user.userName}</TableBodyCell>
                                <TableBodyCell>
                                    {#if userId === user.userId}
                                    <CheckCircleSolid/>
                                    {/if}
                                </TableBodyCell>
                                <TableBodyCell>
                                    {#if userId !== user.userId}
                                    <Button on:click={() => selectedUserId = user.userId} color="red">
                                        <TrashBinSolid/>
                                    </Button>
                                    {/if}
                                </TableBodyCell>
                            </TableBodyRow>
                            {/each}
                        </TableBody>
                    </Table>
                </div>
                <div class="block">
                    <Table>
                        <TableHead class="text-white dark:text-white bg-primary-500">
                            <TableHeadCell>Join Code</TableHeadCell>
                            <TableHeadCell>Uses left</TableHeadCell>
                            <TableHeadCell>
                                <Button
                                color="primary"
                                on:click={() => showJoinKeyModel = true}>
                                <PlusSolid class="w-4 h-4 mr-4 text-white"/>
                                create new code
                            </Button>
                            </TableHeadCell>
                        </TableHead>
                        <TableBody>
                            {#each session.joinKeys as joinCode}
                            <TableBodyRow>
                                <TableBodyCell data-testid="join-key">{joinCode.joinKey}</TableBodyCell>
                                <TableBodyCell>{joinCode.usesLeft}</TableBodyCell>
                                <TableBodyCell>
                                    <Button on:click={() => copyJoinCode(joinCode.joinKey)} color="primary">
                                        <ClipboardCheckSolid/>
                                    </Button>
                                    <Button on:click={() => selectedJoinCode = joinCode} color="red">
                                        <TrashBinSolid/>
                                    </Button>
                                </TableBodyCell>
                            </TableBodyRow>
                            {/each}
                        </TableBody>
                    </Table>
                </div>
            </div>
        </TabItem>
        {/if}
    </Tabs>

    <Toast
    class="absolute mb-4"
    position="top-right"
    transition={fly} 
    params={{ x: 200 }} 
    color="blue"
    open={showToast}
    on:close={() => showToast = false}>
        <p>code copied</p>
    <InfoCircleSolid slot="icon"/>
    </Toast>  

    <Modal
    title="New join code"
    open={showJoinKeyModel}
    on:close={() => showJoinKeyModel = false}>
    <form 
    class="flex items-center flex-col gap-4"
    action="?/create" 
    method="post">
        <div class="flex gap-2">
            <label>amount of uses:<input name="amountOfUses" type="number"/></label>
        </div>
        <input name="sessionId" type="hidden" value="{session.id}"/>
        <input name="createdBy" type="hidden" value="{userId}"/>
        <Button class="flex gap-2" type="submit" color="primary">Create</Button>
    </form>
    </Modal>
    <Modal
    title="Delete join code"
    open={selectedJoinCode !== null}
    on:close={() => selectedJoinCode = null}>
    <form 
    class="flex items-center flex-col gap-4"
    action="?/deletejoin" 
    method="post">
        <p class="flex gap-2">Are you sure you want to remove this join code?</p>
        <input class="flex gap-2" name="selectedGuid" type="hidden" value="{selectedJoinCode?.joinKey}"/>
        <div class="flex gap-2">
            <Button  type="submit" color="primary">Yes</Button>
            <Button on:click={()=> selectedJoinCode = null} color="red">No</Button>
        </div>
    </form>
    </Modal>

    <Modal
    title="Delete session member"
    open={selectedUserId !== null}
    on:close={() => selectedUserId = null}>
    <form 
    class="flex items-center flex-col gap-4"
    action="?/deleteuser" 
    method="post">
        <p class="flex gap-2">Are you sure you want to remove this person from the session?</p>
        <input class="flex gap-2" name="sessionId" type="hidden" value="{session.id}"/>
        <input class="flex gap-2" name="userId" type="hidden" value="{selectedUserId}"/>
        <div class="flex gap-2">
            <Button  type="submit" color="primary">Yes</Button>
            <Button on:click={()=> selectedJoinCode = null} color="red">No</Button>
        </div>
    </form>
    </Modal>
</div>