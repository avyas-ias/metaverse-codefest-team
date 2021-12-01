package com.example.metaversecodefestadserver.controllers;

import com.example.metaversecodefestadserver.controllers.dto.response.AdReportDTO;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Slf4j
@RestController
public class ReportController {

    /**
     * Store view count per ad-session
     */
    private final Map<String, AdReportDTO> viewCounts = new HashMap<>();

    /**
     * API for user to notify whether ad was seen for a particular session
     *
     * @param sessionId session id
     */
    @GetMapping("/report/{sessionId}/{deltaTime}")
    public void getSession(@PathVariable final String sessionId, @PathVariable final Float deltaTime) {
        log.info("Reporting ad-notify beat from session: {}", sessionId);

        if (viewCounts.containsKey(sessionId)) {
            viewCounts.get(sessionId).addViewInfo(deltaTime);
        } else {
            final AdReportDTO report = new AdReportDTO(sessionId);
            report.addViewInfo(deltaTime);
            viewCounts.put(sessionId, report);
        }
    }

    /**
     * API to get view counts for all the sessions for reporting purpose
     *
     * @return view count per session
     */
    @GetMapping("/report")
    public List<AdReportDTO> getReport() {
        return new ArrayList<>(viewCounts.values());
    }


}
