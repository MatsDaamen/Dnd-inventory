<script lang="ts">
import Navbar from '$lib/Components/Navbar.svelte';
import { Button } from 'flowbite-svelte';
import { signIn } from '@auth/sveltekit/client'
import google_logo from '$lib/images/google_logo.svg';
</script>

<Navbar/>

<div class="flex flex-col justify-center items-center h-[80%]">
    <div class="text-2xl font-bold text-center mb-4">
        <h1>Log in</h1>
    </div>
    <Button
        on:click={() => signIn('google')}
        color="primary"
        size="lg"
        class="text-lg"
        iconStyle="!w-5 !h-5"
    >
        <img
            src={google_logo}
            alt="Google logo"
            class="w-8 h-8 p-1 mr-4 rounded-full bg-white"
        />
        Log in with Google
    </Button>
</div>