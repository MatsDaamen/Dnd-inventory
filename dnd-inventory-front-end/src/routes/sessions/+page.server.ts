import type { Actions, PageServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';
import { getSessions, type Session, joinSession, createSession, type requestJoinKey, type sessionCreate, type listSession } from '$lib/API/sessions';
import { getUser, isAuthenticated, user } from '$lib/auth/authStore';

export const load = (async ({ }) => {

    if (!isAuthenticated)
		throw redirect(302, '/login');

    console.log(getUser());
    let userId = "";
    //console.log(userId);

    const sessions: Session[] = await getSessions(userId);

    let sessionList: listSession[] = [];

    for (let i = 0; i < sessions.length; i++) {
        let newSession: listSession = {
            id: sessions[i].id,
            name: sessions[i].name,
            createrName: ""
        }

        sessionList.push(newSession);
    }

    return {
        sessions: sessionList,
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