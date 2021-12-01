package com.example.metaversecodefestadserver.controllers;

import com.example.metaversecodefestadserver.controllers.dto.response.AdReportDTO;
import com.example.metaversecodefestadserver.controllers.dto.response.ReportLogDTO;
import com.example.metaversecodefestadserver.controllers.dto.response.ReportsDTO;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import java.util.*;

@Slf4j
@RestController
public class ReportController {

    /**
     * Store view count per ad-session
     */
    private final Map<String, AdReportDTO> viewCounts = new HashMap<>();

    /**
     * Stores raw mock-qlogs
     */
    private final List<ReportLogDTO> logs = new ArrayList<>();


    /**
     * API for user to notify whether ad was seen for a particular session
     *
     * @param sessionId session id
     */
    @GetMapping("/report/{sessionId}/{deltaTime}")
    public void getSession(@PathVariable final String sessionId, @PathVariable final Float deltaTime) {
        log.info("Reporting ad-notify beat from session: {}", sessionId);

        final AdReportDTO report;

        if (viewCounts.containsKey(sessionId)) {
            report = viewCounts.get(sessionId);
            report.addViewInfo(deltaTime);
        } else {
            report = new AdReportDTO(sessionId);
            report.addViewInfo(deltaTime);
            viewCounts.put(sessionId, report);
        }

        logs.add(new ReportLogDTO(sessionId, report.getViewCount(), report.getAvgViewTime()));
    }

    /**
     * API to get view counts for all the sessions for reporting purpose
     *
     * @return view count per session
     */
    @GetMapping("/report")
    public ReportsDTO getReport() {
        final List<ReportLogDTO> logDuplicate = new ArrayList<>(logs);
        Collections.reverse(logDuplicate);
        return new ReportsDTO(new ArrayList<>(viewCounts.values()), logDuplicate);
    }

}
