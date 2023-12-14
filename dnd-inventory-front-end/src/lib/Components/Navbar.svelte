<script lang='ts'>
    import type { User } from '@auth0/auth0-spa-js';
  import {
      Navbar, NavBrand, NavLi, NavUl, NavHamburger, Avatar, Button, Dropdown, DropdownItem, DropdownHeader
  } from 'flowbite-svelte';
  import { signOut } from '@auth/sveltekit/client';

  export let user: User;

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
  {#if user}
  <div>
    {#if user}
      <Avatar id="avatar-menu" src={user.image ?? ''} />
    {:else}
      <Button href="/login" color="primary">Login</Button>
    {/if}
		<Dropdown placement="bottom" triggeredBy="#avatar-menu">
			<DropdownHeader>
				<span class="block text-sm font-bold">{user.name}</span>
				<span class="block truncate text-sm">{user.email}</span>
			</DropdownHeader>
			<DropdownItem on:click={() => signOut()}>Logout</DropdownItem>
		</Dropdown>
  </div>
  {:else}
    <Button href="/login" color="primary">Login</Button>
  {/if}
</Navbar>