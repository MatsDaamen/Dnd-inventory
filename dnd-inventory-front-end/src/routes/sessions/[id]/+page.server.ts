import type { Actions, PageServerLoad } from './$types';
import { CreateJoinCode, getSession, type Session, type CreationJoinKey, DeleteJoinCode, DeleteUserId } from '$lib/API/sessions';
import { redirect } from '@sveltejs/kit';
import { getUserId, getUserName } from '$lib/API/auth';
import { getAllInventoriesInSession, getInventory, type AddItemRequest, type inventory, AddItemToInventory, type TransferItemRequest, TransferItemToInventory } from '$lib/API/inventory';

export const load = (async ( { locals, params } ) => {

    const loginSession = await locals.getSession();
    if (!loginSession?.user) throw redirect(302, '/login');

    let userId = await getUserId(loginSession.user.email);

    const id = +params.id;
    
    const session: Session = await getSession(id);

    let inventory: inventory[] = session.createdBy === userId ? await getAllInventoriesInSession(session.id) : await getInventory(session.id, userId);

    return {
        session: session,
        userid: userId?.toString(),
        inventory: inventory
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
    }),
    addItem: (async ({request}) => {
        const data = await request.formData();

        const sessionId = data.get("sessionId") as string
        const userId = data.get("userId") as string
        const itemId = data.get("itemId") as string
        const amount = data.get("amountOfItems") as string

        const AddItemRequest: AddItemRequest = {
            sessionId: +sessionId,
            userId: userId,
            itemId: +itemId,
            Amount: +amount
        }

        await AddItemToInventory(AddItemRequest);
    }),
    transferItem: (async ({request}) => {
        const data = await request.formData();

        const sessionId = data.get("sessionId") as string
        const userId = data.get("userId") as string
        const newUserId = data.get("newUserId") as string
        const itemId = data.get("itemId") as string
        const amount = data.get("amountOfItems") as string

        const transferItemRequest: TransferItemRequest = {
            sessionId: +sessionId,
            userId: userId,
            newUserId: newUserId,
            itemId: +itemId,
            Amount: +amount
        }

        await TransferItemToInventory(transferItemRequest);
    })
} satisfies Actions;