import React, { Component } from 'react';
import ias_logo from './ias_logo.jpg';
import './App.css';
import 'semantic-ui-css/semantic.min.css';
import { Container,Image,Header,Card,Divider,Statistic,Table } from 'semantic-ui-react';

class App extends Component {

  constructor(){
    super();
    this.state = {
      items : [],
      logs : []
    }
    setInterval(()=>{this.getCardData();this.getLogData()},1000);
  }


  render() {
    return (
      <div className="App">
          <div className="dash-header">
            <Image src={ias_logo} size='small' />
            <Header as='h1'>AVATAR Dashboard</Header>
          </div>
        <Container textAlign='center'>
          <Card.Group centered itemsPerRow={4} items={this.state.items} />
          <Divider/>
          <Table celled selectable>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Placement Name</Table.HeaderCell>
                <Table.HeaderCell>Time in view</Table.HeaderCell>
                <Table.HeaderCell>No. of views</Table.HeaderCell>
              </Table.Row>
            </Table.Header>

            <Table.Body>
              {[...this.state.logs]}
            </Table.Body>
          </Table>
        </Container>
      </div>
    );
  }

  getCardData = () => {
    fetch("http://localhost:9080/dt/report"
    ,{ method: "GET", mode: 'cors', headers: { 'Content-Type': 'application/json',}})
    .then(response=>{return response.json()})
    .then(data=>{
      let tmp = data['sessionReports'].map( i =>{
        return ({
          content:<Statistic>
          <div className='dash-card-header'>{i['sessionId']}</div>
          <Statistic.Value>{i['viewCount']}</Statistic.Value>
          <Statistic.Label>no. of views</Statistic.Label>
          <Divider/>
          <Statistic.Value>{i['avgViewTime']}</Statistic.Value>
          <Statistic.Label>avg. view time in secs</Statistic.Label>
          </Statistic>,
          color : 'orange',
          className : 'dash-card',
        })
      })
      console.log(tmp);
      this.setState({ items: tmp });
    })
    .catch(error=>console.log(error));    
  }

  getLogData = () => {
    fetch("http://localhost:9080/dt/report"
    ,{ method: "GET", mode: 'cors', headers: { 'Content-Type': 'application/json',}})
    .then(response=>{return response.json()})
    .then(data=>{
      let tmp = data['logReports'].map( i =>{
        return (<Table.Row>
          <Table.Cell>{i['sessionId']}</Table.Cell>
          <Table.Cell>{i['viewTimeTillNow']}</Table.Cell>
          <Table.Cell>{i['viewCountTillNow']}</Table.Cell>
        </Table.Row>)
      })
      console.log(tmp);
      this.setState({ logs: tmp });
    })
    .catch(error=>console.log(error));    
  }
  
}

export default App;
