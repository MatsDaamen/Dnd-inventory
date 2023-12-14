import type { User } from "@auth0/auth0-spa-js";
import { writable, get } from "svelte/store";


export const isAuthenticated = writable(false);
export const user = writable<User>();
export const error = writable();

export function getUser() : User {
  console.log(get(user));
  return get(user);
}

export function GetIsAuthenticated() {
  return get(isAuthenticated);
}