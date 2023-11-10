import type { Actions, PageServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';
import { getSessions, type Session, joinSession, type joinKey, createSession } from '$lib/API/sessions';

export const load = (async () => {

    const sessions: Session[] = await getSessions();

    return {sessions};
}) satisfies PageServerLoad;

export const actions = {
    join: (async ({request, params}) => {

        const data = await request.formData();

        const sessionJoinKey = data.get("joinkey") as string;
        const userId = 10

        const joinkey: joinKey =
        {
            sessionJoinKey,
            userId
        }

        console.log(joinkey)

        await joinSession(joinkey);
    }),
    create: (async ({request, params}) => {

        const data = await request.formData();

        const name = data.get("sessionName") as string;
        const createdBy = 10

        const session: Session =
        {
            id: 0,
            name,
            createdBy
        }

        console.log(session)

        await createSession(session);
    })
} satisfies Actions;