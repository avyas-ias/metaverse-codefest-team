import React, { Component } from 'react'
import { BrowserRouter } from 'react-router-dom'
import './App.css'
import 'semantic-ui-css/semantic.min.css'
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  BarController,
  Legend,
} from 'chart.js'
import {
  IASCardBody,
  IASCard,
  IASCardHeader,
  IASCardRow,
  IASStoreProvider,
  IASGrid,
  IASGridHeader,
  IASGridBody,
  IASNavLayout,
  IASChip,
} from 'react-core'
import { Bar } from 'react-chartjs-2'
class App extends Component {
  constructor() {
    super()
    this.state = {
      items: [],
      segdata: [],
      viewData: [],
      // loading_seg: true,
      loading_view: true,
      chartData: [],
      lablesData: [],
      score: [],
      // tableData:[]
    }
    setInterval(() => {
      this.getViewabilityData()
      // this.getSegmentData()
    }, 5000)
    ChartJS.register(
      CategoryScale,
      LinearScale,
      BarElement,
      BarController,
      Title,
      Tooltip,
      Legend,
    ) 
  }

  // tableData = 

  render() {
    return (
      <IASStoreProvider>
        <BrowserRouter basename="" routes={[]}>
          {/* <h1>{this.state}</h1> */}
          <IASNavLayout basename="" routes={[]}>
            <div style={{ margin: '10px 2%' }}>
              <div className="ias-page-header">
                <div className="ias-header-title-container">
                  <h2>IAS IN METAVERSE</h2>
                </div>
                <div className="ias-header-inputs-container"></div>
              </div>
              <div style={{ margin: '20px 0' }}>
                <IASCardRow cardsPerRow="1">
                  <IASCard>
                    <IASCardHeader title="Viewability Metrics"></IASCardHeader>
                    <IASCardBody>
                      <IASGrid
                        rowHeight={60}
                        columns={[
                          {
                            Header: 'Advertisement ID',
                            accessor: 'id',
                          },
                          {
                            Header: 'Time in View (in Sec)',
                            accessor: 'viewability',
                          },
                          {
                            Header: 'Cumulative Viewability (> 1 Sec)',
                            accessor: 'cumulative',
                            Cell: ({ value }) => (
                              <div>
                                {value < 1 ? (
                                  <IASChip
                                    chipType="status"
                                    label={value}
                                    theme="error"
                                  />
                                ) : (
                                  value
                                )}
                              </div>
                            ),
                          },
                          {
                            Header: 'Angle in View (< 55 degree)',
                            accessor: 'angle',
                            Cell: ({ value }) => (
                              <div>
                                {value > 55 ? (
                                  <IASChip
                                    chipType="status"
                                    label={value}
                                    theme="error"
                                  />
                                ) : (
                                  value
                                )}
                              </div>
                            ),
                          },
                          {
                            Header: ' Real Estate (> 1 Pixel percentage)',
                            accessor: 'pixel',
                            Cell: ({ value }) => (
                              <div>
                                {value < 1.1 ? (
                                  <IASChip
                                    chipType="status"
                                    label={value}
                                    theme="error"
                                  />
                                ) : (
                                  value
                                )}
                              </div>
                            ),
                          },
                          {
                            Header: 'Viewable(True/False)',
                            accessor: 'viewable',
                            Cell: ({ value }) => (
                              <div>
                                {value == 'true' ? (
                                  <IASChip
                                    chipType="status"
                                    label={value}
                                    theme="built"
                                  />
                                ) : (
                                  <IASChip
                                    chipType="status"
                                    label={value}
                                    theme="error"
                                  />
                                )}
                              </div>
                            ),
                          },
                        ]}
                        height={350}
                        isGridLoading={this.state.loading_view}
                        items={[...this.state.viewData]}
                        onChange={function noRefCheck() {}}
                      >
                        <IASGridHeader />
                        <IASGridBody />
                      </IASGrid>
                    </IASCardBody>
                  </IASCard>
                </IASCardRow>
              </div>

              <IASCardRow cardsPerRow="1">
                <IASCard>
                  <IASCardHeader title="Contexual Segment Metrics"></IASCardHeader>
                  <IASCardBody>
                    <IASGrid
                      rowHeight={60}
                      columns={[
                        {
                          Header: 'Object Name',
                          accessor: 'id',
                          width: 80,
                        },
                        {
                          Header: 'Targeted Segments',
                          accessor: 'segment_targeted',
                          width: 450,
                          Cell: ({ value }) => (
                            <div
                              style={{
                                display: 'flex',
                                overflowX: 'scroll',
                                scrollbarWidth: 'none',
                                msOverflowStyle: 'none',
                                scrollBehavior: 'smooth',

                                margin: 6,
                              }}
                            >
                              {value?.map((targeting, _idx) => {
                                return (
                                  <IASChip
                                    active
                                    key={_idx}
                                    chipType="status"
                                    label={targeting || 'NA'}
                                    theme="built"
                                    style={{ width: 100 }}
                                  />
                                )
                              })}
                            </div>
                          ),
                        },
                        {
                          Header: 'Avoided Segments',
                          accessor: 'segment_avoided',
                          width: 150,
                          Cell: ({ value }) => (
                            <div
                              style={{
                                display: 'flex',
                                overflowX: 'scroll',
                                scrollbarWidth: 'none',
                                msOverflowStyle: 'none',
                                margin: 6,
                              }}
                            >
                              {value?.map((avoidance, _idx) => {
                                return (
                                  <IASChip
                                    ready
                                    key={_idx}
                                    chipType="status"
                                    isFixedWidth
                                    label={avoidance || 'NA'}
                                    theme="error"
                                  />
                                )
                              })}
                            </div>
                          ),
                        },
                      ]}
                      height={350}
                      // isGridLoading={this.state.loading_seg}
                      isGridLoading={this.state.loading_view}
                      items={[...this.state.segdata]}
                      onChange={function noRefCheck() {}}
                    >
                      <IASGridHeader />
                      <IASGridBody />
                    </IASGrid>
                  </IASCardBody>
                </IASCard>
              </IASCardRow>
              <div style={{ margin: '20px 0' }}>
                <IASCardRow cardsPerRow="1">
                  <IASCard>
                    <IASCardHeader title="Brand safety Graph"></IASCardHeader>
                    <IASCardBody>
                      <Bar
                        data={
                          {
                            labels: [
                              'Spam',
                              'Terrorism',
                              'Crime',
                              'Dim',
                              'Obscenity',
                              'Piracy',
                              'Sensitive',
                              'Arms',
                              'Hate',
                              'Adult',
                              'Dteva',
                            ],
                            // labels: [...this.state.lablesData],
                            datasets: [
                              {
                                // label: 'GARM categories',
                                backgroundColor: [
                                  'rgba(255, 99, 132, 0.2)',
                                  'rgba(255, 159, 64, 0.2)',
                                  'rgba(255, 205, 86, 0.2)',
                                  'rgba(75, 192, 192, 0.2)',
                                  'rgba(54, 162, 235, 0.2)',
                                  'rgba(153, 102, 255, 0.2)',
                                  'rgba(201, 203, 207, 0.2)',
                                ],
                                borderColor: [
                                  'rgb(255, 99, 132)',
                                  'rgb(255, 159, 64)',
                                  'rgb(255, 205, 86)',
                                  'rgb(75, 192, 192)',
                                  'rgb(54, 162, 235)',
                                  'rgb(153, 102, 255)',
                                  'rgb(201, 203, 207)',
                                ],
                                borderWidth: 2,
                                // data: [0.8, 0.1, 0.2, 0.7, 1, 0.4, 0.6, 0.5, 0.6, 0.5, 0.9],
                                data: [...this.state.score]
                              },
                            ],
                          }
                        }
                        options={{
                          plugins: {
                            title: {
                              display: true,
                              // text: 'GARM categories',
                              fontSize: 20,
                            },
                            legend: {
                              display: false,
                              position: 'bottom',
                            },
                          },
                        }}
                      />
                    </IASCardBody>
                  </IASCard>
                </IASCardRow>
              </div>
            </div>
          </IASNavLayout>
        </BrowserRouter>
      </IASStoreProvider>
    )
  }

  getViewabilityData = () => {
    var requestOptions = {
      method: 'GET',
      redirect: 'follow',
    }

    fetch('http://192.168.179.157:3000/report', requestOptions)
      .then((response) => {
        return response.json()
      })
      .then((data) => {

        let scores_data_list = []
        let total_scores = [0,0,0,0,0,0,0,0,0,0,0]
        data['scene'].forEach(element => {
          scores_data_list.push(element['garm'].map(item => item['score']))
        });
        scores_data_list.map(score_data=>{
          score_data.map((score,idx)=>{
            total_scores[idx]+= parseInt(score)
          })
        });
        total_scores.map(score=>{
          score = Math.floor(score/data['scene'].length)
        })

        this.setState({
          loading_view: false,
        })
        let tmp = data['viewability'].map((i) => {
          return {
            id: i['adName'],
            viewability: i['deltaTime'],
            cumulative: i['cumulativeViewTime'],
            angle: i['angle'],
            pixel: i['realEstate'],
            viewable: i['isVisible'].toString(),
          }
        })
        this.setState({
          viewData: tmp,
        })

        let tmp1 = data['scene'].map((i) => {
          return {
            id: i['objectName'],
            segment_targeted: i['segment'].avoidance,
            segment_avoided: i['segment'].targeting,
          }
        })
        this.setState({
          segdata: tmp1,
        })
        this.setState({
          score: total_scores
        })
        console.log(this.state.score);
      })
      .catch((error) => console.log('error', error))
  }
}

export default App
