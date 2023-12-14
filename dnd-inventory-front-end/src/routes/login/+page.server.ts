import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';
import { GetIsAuthenticated } from '$lib/auth/authStore';

export const load: PageServerLoad = (async ({ }) => {
	
	if (GetIsAuthenticated()){
		throw redirect(302, '/sessions');
	}
})