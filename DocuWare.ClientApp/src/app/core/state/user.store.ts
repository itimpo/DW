import { Injectable } from '@angular/core';
import { createStore, select, withProps } from '@ngneat/elf';
import { localStorageStrategy, persistState } from '@ngneat/elf-persist-state';


interface UserProps {
  user?: string | null;
  participateIds: number[];
  token?: string;
}

const userStore = createStore(
  { name: 'user' },
  withProps<UserProps>({ participateIds:[] })
);

//save in local storage
export const persist = persistState(userStore, {
  key: 'auth',
  storage: localStorageStrategy,
});

userStore.subscribe((state) => console.log(state));
persist.initialized$.subscribe(console.log);

@Injectable({ providedIn: 'root' })
export class UserStore {

  store$ = userStore;

  user$ = userStore.pipe(select((state) => state.user));

  token$ = userStore.pipe(select(state => state.token));

  participateIds$ = userStore.pipe(select(state => state.participateIds));

  get current(): UserProps {
    return userStore.getValue();
  }

  get isAuthenticated(): boolean {
    return userStore.getValue().token != null;
  }

  setUser(user: string | null) {
    userStore.update((state) => ({
      ...state,
      user
    }));
  }

  setToken(token?: string ) {
    userStore.update((state) => ({
      ...state,
      token,
    }));
  }

  setParticipateIds(eventIds: number[]) {
    userStore.update((state) => ({
      ...state,
      participateIds: [...state.participateIds, ...eventIds]
    }));
  }
}
