import { getAccessToken } from '$lib/API/auth';
import type { LayoutServerLoad } from './$types';
import { SetAuthHeaders } from '$lib/API/auth';


export const load = (async ({ locals }) => {
	const session = await locals.getSession();

	SetAuthHeaders(await getAccessToken());

	return { session };
}) satisfies LayoutServerLoad;
