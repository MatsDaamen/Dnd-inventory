import { getUser } from '$lib/auth/authStore';
import type { LayoutServerLoad } from './$types';


export const load = (async ({ }) => {

	return {
		user: await getUser()
	};
}) satisfies LayoutServerLoad;
