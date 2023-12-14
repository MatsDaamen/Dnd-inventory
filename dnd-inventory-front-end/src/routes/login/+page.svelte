<script lang="ts">
import { Button } from 'flowbite-svelte';
import { signIn } from '@auth/sveltekit/client';

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
						on:click={() => signIn('auth0', {
							redirect: false,
							callbackUrl: 'http://localhost:4173/login'
						  },
						  {
							scope: 'api openid profile email'
						  })}
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
