import { SvelteKitAuth } from "@auth/sveltekit"
import Google from "@auth/core/providers/google"
import { sequence } from '@sveltejs/kit/hooks';
import type { Handle } from '@sveltejs/kit';
import { MongoDBAdapter } from "@auth/mongodb-adapter";
import clientPromise from "$lib/database/clientPromise";


export const defaultHandle: Handle = async ({ event, resolve }) => {
	const response = await resolve(event);
	return response;
};

export const authHandle: Handle = SvelteKitAuth(async () => {
	const authOptions = {
		providers: [
			Google({
				clientId: import.meta.env.VITE_GOOGLE_CLIENT_ID,
				clientSecret: import.meta.env.VITE_GOOGLE_SECRET
			})
		],
		adapter: MongoDBAdapter(clientPromise, {
			databaseName: 'accounts'
		}),
		secret: import.meta.env.VITE_SECRET,
		trustHost: true
	};

	return authOptions;
});

export const handle: Handle = sequence(defaultHandle, authHandle);