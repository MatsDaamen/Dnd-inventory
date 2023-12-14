import type { Actions, PageServerLoad } from './$types';
import { CreateJoinCode, getSession, type Session, type CreationJoinKey, DeleteJoinCode, DeleteUserId } from '$lib/API/sessions';
import { redirect } from '@sveltejs/kit';
import { getUserId } from '$lib/API/auth';

export const load = (async ( { locals, params } ) => {

    const loginSession = await locals.getSession();
    if (!loginSession?.user) throw redirect(302, '/login');

    let userId = await getUserId(loginSession.user.email);

    const id = +params.id;
    
    const session: Session = await getSession(id);
    
    return {
        session: session,
        userid: userId?.toString()
    };
}) satisfies PageServerLoad;

export const actions = {
    create: (async ({request}) => {

        const data = await request.formData();

        const sessionId = data.get("sessionId") as string;
        const amountOfUses = data.get("amountOfUses") as string;
        const createdBy = data.get("createdBy") as string;

        const joinkey: CreationJoinKey = {
            sessionId: +sessionId,
            amountOfUses: +amountOfUses,
            createdBy: createdBy
        };

        await CreateJoinCode(joinkey);
    }),
    deletejoin: (async ({request}) => {
        const data = await request.formData();

        const guid = data.get("selectedGuid") as string

        if (guid === null){
            console.log("null value in selected guid");
            return;
        }
        await DeleteJoinCode(guid);
    }),
    deleteuser: (async ({request}) => {
        const data = await request.formData();

        const sessionId = data.get("sessionId") as string
        const id = data.get("userId") as string

        await DeleteUserId(+sessionId, id);
    })
} satisfies Actions;