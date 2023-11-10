<script lang="ts">
    import Navbar from '$lib/Components/Navbar.svelte';
	import {
		Button,
		Modal,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';
    import type { PageData } from './$types'
    import type { Session } from '$lib/API/sessions';

    export let data: PageData;
	const sessions: Session[] = data.sessions;

    let showCreateModal = false;
    let showJoinModal = false;
</script>

<Navbar/>

<div class="grid grid-rows-[max-content,1fr] text-xs md:text-base lg:text-lg p-4 md:p-12 gap-4">
	<div class="flex flex-col md:flex-row gap-4 md:gap-0 md:justify-between">
		<div class="block">
			<h1 class="text-lg md:text-2xl font-bold">Sessions</h1>
		</div>
        <div class="block">
            <Button 
            color="primary"
            on:click={() => showCreateModal = true}>
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
        <TableHead>
            <TableHeadCell>Name</TableHeadCell>
            <TableHeadCell>To session</TableHeadCell>
        </TableHead>
        <TableBody>
            {#each sessions as session}
            <TableBodyRow>
                <TableBodyCell>{session.name}</TableBodyCell>
                <TableBodyCell>
                    <a href="/{session.id}">To Session</a>
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
            <labl>Name:</labl>
            <input name="sessionName" type="text"/>
            <Button type="submit" color="primary">Create</Button>
        </form>
    </Modal>

    <Modal
        title="Join Session"
        open={showJoinModal}
        on:close={() => showJoinModal = false}>
        <form action="?/join" method="post">
            <labl>Join Key:</labl>
            <input name="joinkey" type="text"/>
            <Button type="submit" color="primary">Join</Button>
        </form>
    </Modal>
</div>