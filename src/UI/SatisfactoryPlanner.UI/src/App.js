import './App.css';

import Login from "./Login";
import Registration from './Registration';
import ConfirmRegistration from './ConfirmRegistration';

import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

function App() {
  return (
      <Router>
          <div className="App">
              <Routes>
                  <Route path="/" element={<Login />} />
                  <Route path="/login" element={<Login />} />
                  <Route path="/registration" element={<Registration />} />
                  {/*<Route path="/home" element={<Home />} />*/}
                  <Route exact path="/confirm-registration/:registrationId" element={<ConfirmRegistration />} />
              </Routes>
          </div>

      </Router>
  );
}

export default App;
