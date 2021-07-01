import App from './App.svelte';

const CLI = new App({
  target: document.body,
  props: {
   name: 'this world is yours'
  }
});

window.web = CLI;
export default App;