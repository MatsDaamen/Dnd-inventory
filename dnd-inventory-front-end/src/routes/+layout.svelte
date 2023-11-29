<script lang="ts">
  import "../app.postcss";

  import Navbar from "$lib/Components/Navbar.svelte";
  import type { LayoutData } from "./$types";
  import type { Session } from "@auth/core/types";


  export let data: LayoutData;
  const session: Session | null = data.session;
</script>

  <Navbar {session} />
  
  <slot />
