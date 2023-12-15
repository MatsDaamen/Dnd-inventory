import { redirect } from "@sveltejs/kit";

let headers: HeadersInit;

function getBaseUrl(): string {
        return import.meta.env.VITE_API_URL + `/Session`
}

export function SetAuthHeaders(token: string) {

    headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    }
}

export type listSession = {
    id: number,
    name: string,
    createrName: string
}

export type sessionCreate = {
    name: string,
    createdBy: string
}

export type Session = {
    id: number,
    name: string,
    createdBy: string,
    joinKeys: joinKey[],
    users: sessionUsers[],
};

export type joinKey = {
    joinKey: string,
    usesLeft?: number,
    userId: string
};

export type requestJoinKey = {
    sessionJoinKey: string,
    userId: string
};

export type CreationJoinKey = {
    sessionId: number,
    amountOfUses: number,
    createdBy: string
};

export type sessionUsers = {
    sessionId: number,
    userId: string,
    userName: string
};

export const getSessions = async (userId: string | null): Promise<Session[]> => {

    let url = getBaseUrl();

    if(userId) {
        url = url + "?" + new URLSearchParams({
            userId: userId
        })
    }

	const response = await fetch(url, 
    {
        method: 'GET',
        headers: headers,

    });

	if (response.ok) {
		const sessions: Session[] = await response.json();
		return sessions;
	}

	return [];
};

export const getSession = async (id: number): Promise<Session> => {
	const response = await fetch(getBaseUrl() + `/${id}`, {
        method: 'GET',
        headers: headers,
        
    });

	if (response.ok) {
		const session: Session = await response.json();
		return session;
	}

	throw redirect(302, '/sessions');
};

export const createSession = async (session: sessionCreate) => {
    
        const response = await fetch(getBaseUrl(), {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(session)
        });
    
        return [];
    };

export const joinSession = async (joinRequestDto: requestJoinKey) => {
    
	const response = await fetch(getBaseUrl() + `/join`, {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(joinRequestDto)
    });

	return [];
};

export const CreateJoinCode = async (createionJoinkey: CreationJoinKey) => {
    const response = await fetch(getBaseUrl() + "/JoinKey", {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(createionJoinkey)
    });

	return [];
}

export const DeleteJoinCode = async (guid: string) => {
    const response = await fetch(getBaseUrl() + `/JoinKey/${guid}`, {
        method: 'Delete',
        headers: headers
    });

	return [];
}

export const DeleteUserId = async (sessionid: number, id: string) => {
    const response = await fetch(getBaseUrl() + `/user/${sessionid}/${id}`, {
        method: 'Delete',
        headers: headers
    });

	return [];
}