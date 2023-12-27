import type { Actions, PageServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';
import { getSessions, type Session, joinSession, createSession, type requestJoinKey, type sessionCreate, type listSession } from '$lib/API/sessions';
import { getAccessToken, getUserId } from '$lib/API/auth';

export const load = (async ({ locals }) => {

    const session = await locals.getSession();
    if (!session?.user) throw redirect(302, '/login');

    let userId = await getUserId(session.user.email);

    const sessions: listSession[] = await getSessions(userId);

    return {
        sessions: sessions,
        userid: userId?.toString()
        };
    
}) satisfies PageServerLoad;

export const actions = {
    join: (async ({request}) => {

        const data = await request.formData();

        const sessionJoinKey = data.get("joinkey") as string;
        const userId = data.get("userid") as string;

        const sessionJoinkey: requestJoinKey =
        {
            sessionJoinKey,
            userId
        }

        await joinSession(sessionJoinkey);
    }),
    create: (async ({request}) => {

        const data = await request.formData();

        const name = data.get("sessionName") as string;
        const createdBy = data.get("createdBy") as string;

        const session: sessionCreate =
        {
            name,
            createdBy
        }

        await createSession(session);
    })
} satisfies Actions;