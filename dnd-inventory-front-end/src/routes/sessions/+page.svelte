<script lang="ts">
	import {
		Button,
		Modal,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell,
        Tooltip
	} from 'flowbite-svelte';
    import { PlusSolid, OpenBookSolid } from 'flowbite-svelte-icons';
    import type { PageData } from './$types'
    import type { listSession } from '$lib/API/sessions';

    export let data: PageData;
	const sessions: listSession[] = data.sessions;
    const userId: string = data.userid;

    let showCreateModal = false;
    let showJoinModal = false;
</script>

<div class="grid grid-rows-[max-content,1fr] text-xs md:text-base lg:text-lg p-4 md:p-12 gap-4">
	<div class="flex flex-col md:flex-row gap-4 md:gap-0 md:justify-between">
		<div class="block">
			<h1 class="text-lg md:text-2xl font-bold">Sessions</h1>
		</div>
        <div class="block">
            <Button 
            color="primary"
            on:click={() => showCreateModal = true}>
                <PlusSolid class="w-4 h-4 mr-4 text-white"/>
                Create new Session
            </Button>
            <Button 
            color="primary"
            on:click={() => showJoinModal = true}>
                Join Session
            </Button>
        </div>
    </div>
    <Table>
        <TableHead class="bg-primary-500">
            <TableHeadCell>Name</TableHeadCell>
            <TableHeadCell>session owner</TableHeadCell>
            <TableHeadCell>To session</TableHeadCell>
        </TableHead>
        <TableBody>
            {#each sessions as session}
            <TableBodyRow data-testid="session-table">
                <TableBodyCell>{session.name}</TableBodyCell>
                <TableBodyCell>{session.createrName}</TableBodyCell>
                <TableBodyCell>
                    <Button color="primary" href="/sessions/{session.id}">
                    <OpenBookSolid />
                    </Button>
                    <Tooltip>Go to session</Tooltip>
                </TableBodyCell>
            </TableBodyRow>
            {/each}
        </TableBody>
    </Table>

    <Modal
        title="create new Session"
        open={showCreateModal}
        on:close={() => showCreateModal = false}>
        <form action="?/create" method="post">
            <label>Name: <input name="sessionName" type="text"/></label>
            <input name="createdBy" type="hidden" value="{userId}"/>
            <Button type="submit" color="primary">Create</Button>
            <Button on:click={()=> showCreateModal = false} color="red">Cancel</Button>
        </form>
    </Modal>

    <Modal
        title="Join Session"
        open={showJoinModal}
        on:close={() => showJoinModal = false}>
        <form action="?/join" method="post">
            <label>Join Key:<input name="joinkey" type="text"/></label>
            <input name="userid" type="hidden" value="{userId}"/>
            <Button type="submit" color="primary">Join</Button>
            <Button on:click={()=> showJoinModal = false} color="red">Cancel</Button>
        </form>
    </Modal>
</div>