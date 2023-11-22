import type { RequestHandler } from '../$types';
import type { Session } from '@auth/core/types';
import { error } from '@sveltejs/kit';

export const GET: RequestHandler = async ({ locals, url }) => {


    console.log(locals);


	const session: Session | null = await locals.getSession();

	if (!session?.user?.email) {
		throw error(401, 'Unauthorized')
	}

	return new Response(null, {
		status: 401,
		headers: {
			'content-type': 'application/json'
		}
	});
};