import type { PageServerLoad } from './$types';
import { getSession, type Session } from '$lib/API/sessions';

export const load = (async ( params ) => {

    const id: number = params.id;

    const session = getSession(id);

    return {session};
}) satisfies PageServerLoad;