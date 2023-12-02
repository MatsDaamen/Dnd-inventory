import type { LayoutServerLoad } from './$types';
import type { Session } from '@auth/core/types';
import { UserDatabase, type UserAccount } from '$lib/database/userDatabase';
import clientPromise from '$lib/database/clientPromise';
import { SetAuthHeaders } from '$lib/API/sessions';

export const load = (async ({ locals }) => {
	const session = await locals.getSession();
	let user = await getUser(session);

	return {
		session: structuredClone(session),
		user: structuredClone(user)
	};
}) satisfies LayoutServerLoad;

async function getUser(session: Session | null): Promise<UserAccount | null> {
	if (session === null || !session.user || !session.user.email) {
		return null;
	}

    const userDatabase: UserDatabase = await UserDatabase.fromClient(clientPromise);
    const user = await userDatabase.getUserByEmail(session.user.email);

	if (user != null && user._id != null) {
		const token = await userDatabase.getAccountTokenByUserId(user._id);
		SetAuthHeaders(token);
	}




    return user;
}