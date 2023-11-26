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
				clientId: "434241718547-2drnn06aiee8f820j0fc4bqj5i3pru84.apps.googleusercontent.com",//import.meta.env.VITE_GOOGLE_CLIENT_ID,
				clientSecret: "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"//import.meta.env.VITE_GOOGLE_SECRET
			})
		],
		adapter: MongoDBAdapter(clientPromise, {
			databaseName: 'accounts'
		}),
		secret: "bd05789392e962f687ed468a66de0319",//import.meta.env.VITE_SECRET,
		trustHost: true
	};

	return authOptions;
});

export const handle: Handle = sequence(defaultHandle, authHandle);