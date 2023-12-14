<script lang="ts">
import { Button } from 'flowbite-svelte';
import auth from "$lib/auth/auth";
import { onMount } from 'svelte';
import { isAuthenticated, user } from '$lib/auth/authStore';
import type { Auth0Client } from '@auth0/auth0-spa-js';

let auth0Client: Auth0Client;

onMount(async () => {
    auth0Client = await auth.createClient();
    isAuthenticated.set(await auth0Client.isAuthenticated());
    user.set(await auth0Client.getUser());
  });

  function login() {
    auth.login(auth0Client);
  }

  function logout() {
    auth.logout(auth0Client);
  }
</script>

<div class="h-screen">
	<div class="flex bg-[#F7F7F7] h-full">
		<div class="lg:w-[28rem] w-full p-16 bg-white h-full">
			<div class="h-full">
				<div class="flex flex-col justify-center items-center h-[80%]">
					<div class="text-2xl font-bold text-center mb-4">
						<h1>Log in</h1>
					</div>
					<Button
						on:click={() => login()}
						color="primary"
						size="lg"
						class="text-lg"
						iconStyle="!w-5 !h-5"
					>
						Log in
					</Button>
				</div>
			</div>
		</div>
		<div class="lg:flex hidden flex-grow justify-center items-center">

		</div>
	</div>
</div>
