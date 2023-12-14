import { SetAuthHeaders } from "./sessions";

let headers = {
    'Content-Type': 'application/json',
    'authorization': 'Bearer ' + import.meta.env.VITE_AUTH_API_TOKEN
}

function getBaseUrl(): string {
        return import.meta.env.VITE_AUTH0_DOMAIN
}

export const getUserId = async (email: string): Promise<string | null> => {

    let url = getBaseUrl() + '/api/v2/users-by-email?'  + new URLSearchParams({email: email});

	const response = await fetch(url, 
    {
        method: 'GET',
        headers: headers,

    });

	if (response.ok) {
        const responseBody: [{ user_id: string}] = await response.json();
        let user = responseBody.pop();

        let id = user?.user_id;

        SetAuthHeaders('');

        return id;
	}

	return null;
};

