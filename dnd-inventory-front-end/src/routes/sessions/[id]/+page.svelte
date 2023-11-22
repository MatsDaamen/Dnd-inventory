<script lang="ts">
    import type { PageData } from './$types';
    import Navbar from '$lib/Components/Navbar.svelte';
    import {
		Button,
		Modal,
        Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell,
        Toast
	} from 'flowbite-svelte';
    import { PlusSolid, TrashBinSolid, ClipboardCheckSolid, InfoCircleSolid } from 'flowbite-svelte-icons';
    import type { Session, joinKey } from '$lib/API/sessions';
    import { fly } from 'svelte/transition';

    export let data: PageData;

    const session: Session = data.session;

    const userId = 10;

    let selectedJoinCode: joinKey | null = null;

    let showJoinKeyModel = false;

    let showToast = false;

    function copyJoinCode(joinCode: string){
        navigator.clipboard.writeText(joinCode);
        showToast = true;
        console.log(showToast);
    }

</script>

<Navbar/>

<div class="grid grid-rows-[max-content,1fr] text-xs md:text-base lg:text-lg p-4 md:p-12 gap-4">

	<div class="flex flex-col md:flex-row gap-4 md:gap-0 md:justify-between">
		<div class="block">
			<h1 class="text-lg md:text-2xl font-bold">Sessions</h1>
		</div>
        {#if session.createdBy === userId}
        <div class="block">
            <Button 
                color="primary"
                on:click={() => showJoinKeyModel = true}>
                <PlusSolid class="w-4 h-4 mr-4 text-white"/>
                create new code
            </Button>
        </div>
        {/if}

    </div>
    <div class="flex flex-col md:flex-row gap-4 md:gap-0 md:justify-between">
		<div class="block">
		</div>
        {#if session.createdBy === userId}
        <div class="block">  
            <Table>
                <TableHead class="text-white dark:text-white bg-primary-500 dark:bg-primary-600">
                    <TableHeadCell>Join Code</TableHeadCell>
                    <TableHeadCell>Uses left</TableHeadCell>
                    <TableHeadCell>actions</TableHeadCell>
                </TableHead>
                <TableBody>
                    {#each session.joinKeys as joinCode}
                    <TableBodyRow>
                        <TableBodyCell>{joinCode.joinKey}</TableBodyCell>
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
        {/if}
    </div>

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
            <p>amount of uses:</p>
            <input name="amountOfUses" type="number"/>
        </div>
        <input name="sessionId" type="hidden" value="{session.id}"/>
        <input name="createdBy" type="hidden" value="10"/>
        <Button class="flex gap-2" type="submit" color="primary">Create</Button>
    </form>
    </Modal>
    <Modal
    title="Delete join code"
    open={selectedJoinCode !== null}
    on:close={() => selectedJoinCode = null}>
    <form 
    class="flex items-center flex-col gap-4"
    action="?/delete" 
    method="post">
        <p class="flex gap-2">Are you sure you want to remove this join code?</p>
        <input class="flex gap-2" name="selectedGuid" type="hidden" value="{selectedJoinCode?.joinKey}"/>
        <div class="flex gap-2">
            <Button  type="submit" color="primary">Yes</Button>
            <Button on:click={()=> selectedJoinCode = null} color="red">No</Button>
        </div>
    </form>
    </Modal>
</div>