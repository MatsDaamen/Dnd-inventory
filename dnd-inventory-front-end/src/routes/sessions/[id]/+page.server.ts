import type { Actions, PageServerLoad } from './$types';
import { CreateJoinCode, getSession, type Session, type CreationJoinKey, DeleteJoinCode } from '$lib/API/sessions';
import { UserDatabase } from '$lib/database/userDatabase';
import clientPromise from '$lib/database/clientPromise';
import { redirect } from '@sveltejs/kit';

export const load = (async ( { locals, params } ) => {

    const loginSession = await locals.getSession();

	if (!loginSession?.user?.email)
		throw redirect(302, '/login');

    const userDatabase: UserDatabase = await UserDatabase.fromClient(clientPromise);
    const userId = await userDatabase.getUserIdByEmail(loginSession.user.email);

    const id = +params.id;
    
    const session: Session = await getSession(id);

    return {
        session: session,
        userid: userId?.toString()
    };
}) satisfies PageServerLoad;

export const actions = {
    create: (async ({request, params}) => {

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
    delete: (async ({request, params}) => {
        const data = await request.formData();

        const guid = data.get("selectedGuid") as string

        if (guid === null){
            console.log("null value in selected guid");
            return;
        }
        await DeleteJoinCode(guid);
    })
} satisfies Actions;