import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './Components/Layout';
import generate from './Pages/Generate';
import Home from './Pages/Home';
import Upload from './Pages/Upload';




export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/generate' component={generate}/>
        <Route exact path='/upload' component={Upload}/>
     
      </Layout>
    );
  }
}
