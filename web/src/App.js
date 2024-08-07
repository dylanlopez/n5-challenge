import './App.css';
import ListPermissions from './views/ListPermissions';
import RequestPermission from './views/RequestPermission';
import ModifyPermission from './views/ModifyPermission';

function App() {
  return (
    <div className="App">
      <h1>Permissions Management</h1>
      <RequestPermission />
      <ModifyPermission />
      <ListPermissions />
    </div>
  );
}

export default App;
