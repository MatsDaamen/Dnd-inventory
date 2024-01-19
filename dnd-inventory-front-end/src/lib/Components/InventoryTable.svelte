<script lang="ts">
    import type { inventory, item } from '$lib/API/inventory';
    import type { sessionUsers } from '$lib/API/sessions';
    import {
		Button,
		Modal,
        Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell,
        Select,
        Label,
        Tooltip
	} from 'flowbite-svelte';
    import { ForwardSolid } from 'flowbite-svelte-icons';
    import { slide } from 'svelte/transition';

    export let inventory: inventory;

    export let sessionUsers: sessionUsers[];
    let otherUsers = sessionUsers.filter(user => user.userId != inventory?.userId)

    export const inventoryIsOpen: boolean|undefined = false;

    let showItemAddModel : boolean = false;

    let itemSelectedForTransfer : item | null = null;
    let selectedUserId : string;

    let openRow: number | null = null;

	const selectRow = (i: number) => {
		openRow = openRow === i ? null : i;
	};
</script>

<Table data-testid="inventory">
    <TableHead class="text-white dark:text-white bg-primary-500">
        <TableHeadCell>{inventory?.ownerName}'s inventory</TableHeadCell>
        <TableHeadCell>amount</TableHeadCell>
        <TableHeadCell>Actions</TableHeadCell>
    </TableHead>
    <TableBody>      
        {#each inventory.items as item, i}
        <TableBodyRow class="justify-center items-center" on:click={() => selectRow(i)}>
            <TableBodyCell>{item.name}</TableBodyCell>
            <TableBodyCell>{item.amount}</TableBodyCell>
            <TableBodyCell>
                {#if otherUsers.length > 0}    
                <Button on:click={()=> itemSelectedForTransfer = item} color="blue">
                    <ForwardSolid/>
                </Button>
                <Tooltip>transfer</Tooltip>
                {/if}
            </TableBodyCell>
        </TableBodyRow>
        <Tooltip>Click for more info</Tooltip>
        {#if openRow === i}
        <TableBodyRow>
            <TableBodyCell colspan="50" class="p-0">
                <div
                    class="p-6 grid grid-rows-[max-content,max-content,max-content] lg:grid-cols-[12rem,1fr,1fr] gap-8"
                    transition:slide={{ duration: 300, axis: 'y' }}
                >
                <h>
                    discription:
                    <p>{item.discription}</p>
                </h>
                </div>
            </TableBodyCell>
        </TableBodyRow>
        {/if}
        {/each}
    </TableBody>
</Table>

<Modal
title="Add New Item"
open={showItemAddModel}
on:close={() => showItemAddModel = false}>
<form 
class="flex items-center flex-col gap-4"
action="?/addItem" 
method="post">
    <div class="flex gap-2">
        <p>amount of items:</p>
        <input name="amountOfItems" type="number" min="0"/>
    </div>
    <input name="sessionId" type="hidden" value="{inventory.sessionId}"/>
    <input name="userId" type="hidden" value="{inventory.userId}"/>
    <input name="itemId" type="hidden" value="{inventory.userId}"/>
    <Button class="flex gap-2" type="submit" color="primary">Add</Button>
    <Button on:click={()=> showItemAddModel = false} color="red">No</Button>
</form>
</Modal>

<Modal
title="Transfer Item"
open={itemSelectedForTransfer !== null}
on:close={() => itemSelectedForTransfer = null}>
<form 
class="flex items-center flex-col gap-4"
action="?/transferItem" 
method="post">
    <div class="flex gap-2">
    <h>
        item name:
        <p>{itemSelectedForTransfer?.name}</p>
    </h>
    </div>
    <div class="flex gap-2">
        <h>
            item amount:
            <p>{itemSelectedForTransfer?.amount}</p>
        </h>
    </div>
    <div class="flex gap-2">
        <p>amount of items:</p>
        <input name="amountOfItems" type="number" placeholder="1" min="1" max="{itemSelectedForTransfer?.amount}"/>
    </div>
    <input name="sessionId" type="hidden" value="{inventory.sessionId}"/>
    <input name="userId" type="hidden" value="{inventory.userId}"/>
    <input name="itemId" type="hidden" value="{itemSelectedForTransfer?.id}"/>

    <Label>
        Select a new user
        <Select id="newUserId" name="newUserId" class="mt-2" bind:value={selectedUserId}>
            {#each otherUsers as { userId, userName }}
            <option value="{userId}">{userName}</option>
            {/each}
        </Select>
    </Label>

    <Button class="flex gap-2" type="submit" color="primary">transfer</Button>
    <Button on:click={()=> itemSelectedForTransfer = null} color="red">Cancel</Button>
</form>
</Modal>