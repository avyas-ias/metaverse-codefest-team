package com.example.metaversecodefestadserver.controllers.dto.response;

import lombok.Data;

import java.util.List;

@Data
public class ReportsDTO {

    private final List<AdReportDTO> sessionReports;

    private final List<ReportLogDTO> logReports;

}
