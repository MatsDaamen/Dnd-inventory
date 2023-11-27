import { redirect } from "@sveltejs/kit";

function getBaseUrl(): string {
        return `http://localhost:5254/api/Session`
}

export type Session = {
    id: number,
    name: string,
    createdBy: string,
    joinKeys: joinKey[]
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
        headers: {
            'Content-Type': 'application/json'
        },

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

export const joinSession = async (joinRequestDto: requestJoinKey) => {
    
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

export const DeleteJoinCode = async (guid: string) => {
    const response = await fetch(getBaseUrl() + `/JoinKey/${guid}`, {
        method: 'Delete',
        headers: {
            'Content-Type': 'application/json'
        }
    });

	return [];
}