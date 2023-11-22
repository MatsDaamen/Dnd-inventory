import type { Actions, PageServerLoad } from './$types';
import { CreateJoinCode, getSession, type Session, type CreationJoinKey, DeleteJoinCode } from '$lib/API/sessions';

export const load = (async ( { params } ) => {

    const id = +params.id;
    
    const session = await getSession(id);

    console.log(session)

    return {session};
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
            createdBy: +createdBy
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