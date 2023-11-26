import type { LayoutServerLoad } from './$types';
import type { Session } from '@auth/core/types';
import { getuser } from '$lib/API/user';
import type { User } from '$lib/API/user';

export const load = (async ({ locals }) => {
	const session = await locals.getSession();
	let user = await getUser(session);

	return {
		session: structuredClone(session),
		user: structuredClone(user)
	};
}) satisfies LayoutServerLoad;

// This function is used to get the associated user from the session.
async function getUser(session: Session | null): Promise<User | null> {
	if (session === null || !session.user || !session.user.email) {
		return null;
	}

    const user: User | null = await getuser();

    return user;
}