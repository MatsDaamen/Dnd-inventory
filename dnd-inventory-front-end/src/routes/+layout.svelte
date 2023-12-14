<script lang="ts">
  import "../app.postcss";
  import Navbar from "$lib/Components/Navbar.svelte";
  import type { LayoutData } from "./$types";

  export let data: LayoutData;

  let user = data.session?.user
</script>

  <Navbar {user} />
  
  <slot />
