import debug from 'debug';

try {
    // Heads Up!
    // https://github.com/visionmedia/debug/pull/331
    //
    // debug now clears storage on load, grab the debug settings before require('debug').
    // We try/catch here as Safari throws on localStorage access in private mode or with cookies disabled.
    var DEBUG = window.localStorage.debug;
    // enable what ever settings we got from storage
    debug.enable(DEBUG);
} catch (e) {
    console.error('SatisfactoryPlanner could not enable debug.');
    console.error(e);
}

/**
 * Create a namespaced debug function.
 * @param {String} namespace Usually a component name.
 * @example
 * import { makeDebugger } from 'src/lib'
 * const debug = makeDebugger('namespace')
 *
 * debug('Some message')
 * @returns {Function}
 */
const makeDebugger = (namespace: string) => debug(`SFP:${namespace}`);

export default makeDebugger;