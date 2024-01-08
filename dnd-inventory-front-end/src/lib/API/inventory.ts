import { getUserName } from "./auth";

let headers: HeadersInit;

function getBaseUrl(): string {
    return import.meta.env.VITE_API_URL + `/Inventory`
}

export function SetInventoryAuthHeaders(token: string) {
    headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    }
}

export type item = {
    id: number,
    name: string,
    discription: string,
    weight: number,
    price: number,
    amount: number,
    type: string
}

export type inventory = {
    userId: string,
    ownerName: string,
    sessionId: number,
    items: item[]
}

export type AddItemRequest = {
    userId: string,
    sessionId: number,
    itemId: number,
    Amount: number
}

export type TransferItemRequest = {
    userId: string,
    newUserId: string,
    sessionId: number,
    itemId: number,
    Amount: number
}

export const getInventory = async (sessionId: number, userId: string): Promise<inventory[] | null> => {
	
    let url = getBaseUrl() + `/${sessionId}/${userId}`
    
    const response = await fetch(url, {
        method: 'GET',
        headers: headers,
    });

	if (response.ok) {
		const inventories: inventory[] = await response.json();

        for (let i = 0; i < inventories.length; i++) {
            inventories[i].ownerName = await getUserName(inventories[i].userId);
        }

		return inventories;
	}

    return null;
};

export const getAllInventoriesInSession = async (sessionId: number): Promise<inventory[] | null> => {
	
    let url = getBaseUrl() + `/${sessionId}`
    
    const response = await fetch(url, {
        method: 'GET',
        headers: headers,
    });

	if (response.ok) {
		const inventories: inventory[] = await response.json();

        for (let i = 0; i < inventories.length; i++) {
            inventories[i].ownerName = await getUserName(inventories[i].userId);
        }

		return inventories;
	}

    return null;
};

export const AddItemToInventory = async (addItemRequest: AddItemRequest): Promise<null> => {
	
    const response = await fetch(getBaseUrl(), {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(addItemRequest)
    });

	//if (response.ok) {
	//	return inventories;
	//}

    return null;
};

export const TransferItemToInventory = async (transferItemRequest: TransferItemRequest): Promise<null> => {
	
    let url = getBaseUrl() + "/transfer"

    const response = await fetch(url, {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(transferItemRequest)
    });

	//if (response.ok) {
	//	return inventories;
	//}

    return null;
};