import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';

export const load = (async ( { locals } ) => {

    const loginSession = await locals.getSession();
    if (loginSession?.user) throw redirect(302, '/sessions');

}) satisfies PageServerLoad;