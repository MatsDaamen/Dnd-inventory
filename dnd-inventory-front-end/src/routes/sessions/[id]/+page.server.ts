import type { PageServerLoad } from './$types';
import { getSession, type Session } from '$lib/API/sessions';

export const load = (async ( { params } ) => {

    const id = +params.id;
    
    const session = await getSession(id);

    return {session};
}) satisfies PageServerLoad;