<script lang="ts">
    import type { PageData } from './$types';
    import Navbar from '$lib/Components/Navbar.svelte';
    import {
		Button,
		Modal
	} from 'flowbite-svelte';
    import type { Session } from '$lib/API/sessions';

    export let data: PageData;

    const session: Session = data.session;

    let showJoinKeyModel = false;

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
            on:click={() => showJoinKeyModel = true}>
                create new Join Code
            </Button>
        </div>
    </div>

    <Modal
    title="New join code"
    open={showJoinKeyModel}
    on:close={() => showJoinKeyModel = false}>
    <form method="post">
        <p>amount of uses:</p>
        <input name="amountOfUses" type="number"/>
        <input name="sessionId" type="hidden" value="{session.id}"/>
        <input name="createdBy" type="hidden" value="10"/>
        <Button type="submit" color="primary">Create</Button>
    </form>
</Modal>
</div>


