//import { inject } from "@angular/core";
//import { ActivatedRouteSnapshot, CanActivateChildFn, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
//import { catchError, map, of } from "rxjs";
//import { UserStore } from "../core/state/user.store";

//export const canActivate: CanActivateFn = (
//  route: ActivatedRouteSnapshot,
//  state: RouterStateSnapshot
//) => {
//  const userStore = inject(UserStore);
//  const router = inject(Router);

//  return userStore.token$.pipe(
//    map(() => true),
//    catchError(() => {
//      router.navigate(['/']);
//      return of(false);
//    })
//  );
//};

//export const canActivateChild: CanActivateChildFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => canActivate(route, state);
