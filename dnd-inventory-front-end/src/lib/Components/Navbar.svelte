<script lang='ts'>
  import {
      Navbar, NavBrand, NavLi, NavUl, NavHamburger, Avatar, Button, Dropdown, DropdownItem, DropdownHeader
  } from 'flowbite-svelte'
  import type { Session } from '@auth/core/types';
  import { signOut } from '@auth/sveltekit/client'

  export let session: Session | null;

</script>

<Navbar>
  <NavBrand href="/">
    <span class="self-center whitespace-nowrap text-xl font-semibold">
      Dnd inventory
    </span>
  </NavBrand>
  <NavHamburger class="m-0" />
  <NavUl>
    <NavLi href="/" active={true}>Home</NavLi>
    <NavLi href="/sessions">Sessions</NavLi>
  </NavUl>
  {#if session && session.user}
  <div>
    {#if session && session.user}
      <Avatar id="avatar-menu" src={session.user.image ?? ''} />
    {:else}
      <Button href="/login" color="primary">Login</Button>
    {/if}
		<Dropdown placement="bottom" triggeredBy="#avatar-menu">
			<DropdownHeader>
				<span class="block text-sm font-bold">{session.user.name}</span>
				<span class="block truncate text-sm">{session.user.email}</span>
			</DropdownHeader>
			<DropdownItem on:click={() => signOut()}>Logout</DropdownItem>
		</Dropdown>
  </div>
  {:else}
    <Button href="/login" color="primary">Login</Button>
  {/if}
</Navbar>