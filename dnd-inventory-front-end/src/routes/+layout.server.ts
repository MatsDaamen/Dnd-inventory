import { getAccessToken } from '$lib/API/auth';
import { SetAuthHeaders } from '$lib/API/sessions';
import type { LayoutServerLoad } from './$types';


export const load = (async ({ locals }) => {
	const session = await locals.getSession();

	SetAuthHeaders(await getAccessToken());

	return { session };
}) satisfies LayoutServerLoad;
