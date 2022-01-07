import React, { Component } from "react";
import ias_logo from "./ias_logo.jpg";
import "./App.css";
import "semantic-ui-css/semantic.min.css";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { Bar } from "react-chartjs-2";
import {
  Container,
  Image,
  Icon,
  Header,
  Card,
  Divider,
  Statistic,
  Table,
  Grid,
  Segment,
} from "semantic-ui-react";
class App extends Component {
  constructor() {
    super();
    this.state = {
      items: [],
      logs: [],
      sessionIds: [],
      viewTime: [],
      viewCount: [],
    };
    setInterval(() => {
      this.getCardData();
      this.getLogData();
    }, 1000);

    ChartJS.register(
      CategoryScale,
      LinearScale,
      BarElement,
      Title,
      Tooltip,
      Legend
    );
  }

  render() {
    return (
      <div className="App">
        <Container
          fluid
          style={{
            backgroundColor: "#013148",
            textAlign: "left",
          }}
        >
          <Header as="h1">
            <Image src={ias_logo} style={{ height: "8%", width: "8%" }} />
            <Header.Content style={{ color: "white", marginLeft: "30%" }}>
              Metaverse Dashboard
            </Header.Content>
          </Header>
        </Container>
        {/* </div> */}
        <Divider hidden />

        <div style={{ padding: "2%" }} textAlign="center">
          <Card.Group centered itemsPerRow={5} items={this.state.items} />
          <Divider hidden />
          <Grid>
            <Grid.Row>
              <Grid.Column width={8}>
                <Card fluid>
                  <Bar
                    options={{
                      indexAxis: "y",
                      responsive: true,
                      plugins: {
                        legend: {
                          position: "right",
                          display: false,
                        },
                        title: {
                          display: true,
                          text: "Avg View time vs Placement name",
                        },
                      },
                      scales: {
                        yAxes: [
                          {
                            scaleLabel: {
                              display: true,
                              labelString: "probability",
                            },
                          },
                        ],
                      },
                    }}
                    data={{
                      labels: this.state.sessionIds,
                      datasets: [
                        {
                          label: "Avg View Time",
                          data: this.state.viewTime,
                          backgroundColor: "rgba(78, 97, 223, 1)",
                        },
                      ],
                    }}
                  />
                  <h6 style={{ color: "grey" }}>Placement Avg View Time</h6>
                </Card>
              </Grid.Column>
              <Grid.Column width={8}>
                <Card fluid>
                  <Bar
                    options={{
                      indexAxis: "y",
                      responsive: true,
                      plugins: {
                        legend: {
                          position: "right",
                          display: false,
                        },
                        title: {
                          display: true,
                          text: "Avg View Count vs Placement name",
                        },
                      },
                      scales: {
                        yAxes: [
                          {
                            scaleLabel: {
                              display: true,
                              labelString: "probability",
                            },
                          },
                        ],
                      },
                    }}
                    data={{
                      labels: this.state.sessionIds,
                      datasets: [
                        {
                          label: "Avg View Count",
                          data: this.state.viewCount,
                          backgroundColor: "rgba(78, 97, 223, 1)",
                        },
                      ],
                    }}
                  />
                  <h6 style={{ color: "grey" }}>Placement Avg View Count</h6>
                </Card>
              </Grid.Column>
            </Grid.Row>

            <Table celled selectable>
              <Table.Header>
                <Table.Row>
                  <Table.HeaderCell>Placement Name</Table.HeaderCell>
                  <Table.HeaderCell>Time in view</Table.HeaderCell>
                  <Table.HeaderCell>No. of views</Table.HeaderCell>
                </Table.Row>
              </Table.Header>

              <Table.Body>{[...this.state.logs]}</Table.Body>
            </Table>
          </Grid>
        </div>
      </div>
    );
  }

  getCardData = () => {
    // fetch("http://localhost:9080/dt/report"
    //   , { method: "GET", mode: 'cors', headers: { 'Content-Type': 'application/json', } })
    //   .then(response => { return response.json() })
    //   .then(data => {
    let sessionReportsdata = [
      {
        sessionId: "Nike",
        viewCount: 6,
        avgViewTime: 6.0,
      },
      {
        sessionId: "Microsoft",
        viewCount: 7,
        avgViewTime: 4.5,
      },
      {
        sessionId: "Apple",
        viewCount: 6,
        avgViewTime: 2.9,
      },
      {
        sessionId: "Meta",
        viewCount: 6,
        avgViewTime: 6.0,
      },
      {
        sessionId: "IAS",
        viewCount: 7,
        avgViewTime: 4.5,
      },
      {
        sessionId: "Twitter",
        viewCount: 6,
        avgViewTime: 2.9,
      },
      {
        sessionId: "NVDIA",
        viewCount: 6,
        avgViewTime: 2.9,
      },
      {
        sessionId: "Disney",
        viewCount: 7,
        avgViewTime: 4.5,
      },
      {
        sessionId: "addidas",
        viewCount: 6,
        avgViewTime: 2.9,
      },
      {
        sessionId: "H&M",
        viewCount: 6,
        avgViewTime: 2.9,
      },
    ];
    let cnt = -1;
    let tmp = sessionReportsdata.map((i) => {
      var hexArray = ["#8099A5", "#325B6E", "#fcc201", "#4E61DF", "#01B7A9"];
      cnt < 4 ? cnt++ : (cnt = 0);
      var randomColor = hexArray[cnt];
      return {
        content: (
          <Statistic size="mini" style={{ paddingBottom: "10%" }}>
            <div
              className="dash-card-header"
              style={{ backgroundColor: randomColor }}
            >
              {i["sessionId"]}
            </div>
            <Statistic.Value>{i["viewCount"]}</Statistic.Value>
            <Statistic.Label>no. of views</Statistic.Label>
            <Divider />
            <Statistic.Value>{i["avgViewTime"]}</Statistic.Value>
            <Statistic.Label>avg. view time in secs</Statistic.Label>
          </Statistic>
        ),
      };
    });

    this.setState({
      items: tmp,
      sessionIds: sessionReportsdata.map((report) => report.sessionId),
      viewTime: sessionReportsdata.map((report) => report.avgViewTime),
      viewCount: sessionReportsdata.map((report) => report.viewCount),
    });
    // })
    // .catch(error => console.log(error));
  };

  getLogData = () => {
    // fetch("http://localhost:9080/dt/report"
    //   , { method: "GET", mode: 'cors', headers: { 'Content-Type': 'application/json', } })
    //   .then(response => { return response.json() })
    //   .then(data => {
    let logReportsdata = [
      { sessionId: "Nike", viewCountTillNow: 7, viewTimeTillNow: 4.5 },
      { sessionId: "Microsoft", viewCountTillNow: 6, viewTimeTillNow: 4.0 },
      { sessionId: "Addidas", viewCountTillNow: 5, viewTimeTillNow: 3.5 },
      { sessionId: "Disney", viewCountTillNow: 4, viewTimeTillNow: 3.0 },
      { sessionId: "IAS", viewCountTillNow: 3, viewTimeTillNow: 2.5 },
      { sessionId: "Facebook", viewCountTillNow: 6, viewTimeTillNow: 2.9 },
      { sessionId: "Nike", viewCountTillNow: 5, viewTimeTillNow: 2.7 },
      { sessionId: "META", viewCountTillNow: 4, viewTimeTillNow: 2.5 },
      { sessionId: "FOOD CHAIN", viewCountTillNow: 3, viewTimeTillNow: 2.3 },
      { sessionId: "EVENT", viewCountTillNow: 2, viewTimeTillNow: 2.1 },
    ];
    let tmp = logReportsdata.map((i) => {
      return (
        <Table.Row>
          <Table.Cell>{i["sessionId"]}</Table.Cell>
          <Table.Cell>{i["viewTimeTillNow"]}</Table.Cell>
          <Table.Cell>{i["viewCountTillNow"]}</Table.Cell>
        </Table.Row>
      );
    });

    this.setState({
      logs: tmp,
    });
    // this.setState({chartData:data});
  };

  // )
  //     .catch(error => console.log(error));
  // }
}

export default App;
