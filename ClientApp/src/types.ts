export function isArrayOf<T>(obj: unknown, TValidator: (o: unknown) => boolean): obj is T[] {
    if(!Array.isArray(obj)) {
        console.log('not array')
        return false;
    }

    for(const o of obj) {
        if(!TValidator(o)) {
            console.log(`${o} is not T`)
            return false;
        }
    }
    return true;
}