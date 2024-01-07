<script lang="ts">
    import type { inventory } from '$lib/API/inventory';
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
    import { slide } from 'svelte/transition';

    export let inventory: inventory;

    export const inventoryIsOpen: boolean|undefined = false;

    let openRow: number | null = null;

	const selectRow = (i: number) => {
		openRow = openRow === i ? null : i;
	};
</script>

{#if !inventory}
    <h>SHIT FUCKED</h>
{:else}
<Table>
    <TableHead class="text-white dark:text-white bg-primary-500">
        <TableHeadCell>{inventory?.ownerName}'s inventory</TableHeadCell>
    </TableHead>
    <TableBody>
        {#if inventory.items != null}          
        {#each inventory.items as item, i}
        <TableBodyRow class="justify-center items-center" on:click={() => selectRow(i)}>
            <TableBodyCell>{item.name}</TableBodyCell>
        </TableBodyRow>
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
        {/if}
    </TableBody>
</Table>
{/if}