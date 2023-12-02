import clientPromise from "./clientPromise";
import { ObjectId, type Collection, type Db, type MongoClient, type WithId } from 'mongodb';



interface DatabaseUserAccount {
    _id?: ObjectId,
    name: string,
    email: string,
    image: string
}


export interface UserAccount {
    _id?: ObjectId,
    name: string,
    email: string,
    image: string
}

export class UserDatabase {
    private db: Db;
    private accountCollection: Collection<DatabaseUserAccount>;

	constructor(db: Db) {
		this.db = db;
		this.accountCollection = this.db.collection('users');
	}

	static async fromClient(client: Promise<MongoClient>): Promise<UserDatabase> {
		return new UserDatabase((await client).db("accounts"));
	}

    async getUserByEmail(email: string): Promise<UserAccount | null> {
		const account = await this.accountCollection.findOne({ email: email });

        if (!account) return null;

		return {
			_id: account._id,
			name: account.name,
			email: account.email,
			image: account.image
		};
	}

    async getUserIdByEmail(email: string): Promise<string | null> {
		const account = await this.accountCollection.findOne({ email: email });

        if (!account) return null;

		return account._id.toString();
	}
	
	async getUserNameById(id: string): Promise<string | null> {
		const account = await this.accountCollection.findOne({ _id: new ObjectId(id) });

        if (!account) return null;

		return account.name;
	}
}