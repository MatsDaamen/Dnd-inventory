import { MongoClient } from 'mongodb';

function getUri() {
	return import.meta.env.VITE_MONGO_DB;
}

const clientPromise: Promise<MongoClient> = new MongoClient(getUri()).connect();

export default clientPromise;