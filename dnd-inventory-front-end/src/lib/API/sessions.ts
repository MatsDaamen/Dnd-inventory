import { redirect } from "@sveltejs/kit";

function getBaseUrl(): string {
        return `http://localhost:5254/api/Session`
}

export type Session = {
    id: number,
    name: string,
    createdBy: number,
    joinKeys: joinKey[]
};

export type joinKey = {
    joinKey: string,
    usesLeft: number,
    userId: number
};

export type CreationJoinKey = {
    sessionId: number,
    amountOfUses: number,
    createdBy: number
};

export const getSessions = async (): Promise<Session[]> => {


	const response = await fetch(getBaseUrl(), {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
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
        headers: {
            'Content-Type': 'application/json'
        },
        
    });

	if (response.ok) {
        let responseClone = response.clone();

        console.log(await responseClone.json());

		const session: Session = await response.json();

		return session;
	}

	throw redirect(302, '/sessions');
};

export const createSession = async (session: Session) => {
    
        const response = await fetch(getBaseUrl(), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(session)
        });
    
        return [];
    };

export const joinSession = async (joinRequestDto: joinKey) => {
    
	const response = await fetch(getBaseUrl() + `/join`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(joinRequestDto)
    });

	return [];
};

export const CreateJoinCode = async (createionJoinkey: CreationJoinKey) => {
    const response = await fetch(getBaseUrl() + "/JoinKey", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(createionJoinkey)
    });

	return [];
}