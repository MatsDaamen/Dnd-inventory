import type { Actions, PageServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';
import { getSessions, type Session, joinSession, type joinKey, createSession, type requestJoinKey } from '$lib/API/sessions';
import { UserDatabase } from '$lib/database/userDatabase';
import clientPromise from '$lib/database/clientPromise';

export const load = (async ({ locals}) => {

    const session = await locals.getSession();

	if (!session?.user?.email)
		throw redirect(302, '/login');

    const userDatabase: UserDatabase = await UserDatabase.fromClient(clientPromise);
    const userId = await userDatabase.getUserIdByEmail(session.user.email);
    const sessions: Session[] = await getSessions(userId);

    return {
        sessions: sessions,
        userid: userId?.toString()
        };
    
}) satisfies PageServerLoad;

export const actions = {
    join: (async ({request, params}) => {

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
    create: (async ({request, params}) => {

        const data = await request.formData();

        const name = data.get("sessionName") as string;
        const createdBy = data.get("createdBy") as string;

        console.log(createdBy);

        const session: Session =
        {
            id: 0,
            name,
            createdBy,
            joinKeys: []
        }

        await createSession(session);
    })
} satisfies Actions;