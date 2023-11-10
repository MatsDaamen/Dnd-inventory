<script lang='ts'>
    import {
        Navbar, NavBrand, NavLi, NavUl, NavHamburger
    } from 'flowbite-svelte'
</script>

<Navbar let:hidden let:toggle>
  <NavBrand href="/">
    <span class="self-center whitespace-nowrap text-xl font-semibold dark:text-white">
      Dnd inventory
    </span>
  </NavBrand>
  <NavHamburger on:click={toggle} />
  <NavUl {hidden}>
    <NavLi href="/" active={true}>Home</NavLi>
    <NavLi href="/sessions">Sessions</NavLi>
  </NavUl>
</Navbar>