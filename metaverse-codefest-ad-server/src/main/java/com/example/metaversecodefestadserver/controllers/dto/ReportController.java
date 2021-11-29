package com.example.metaversecodefestadserver.controllers.dto;

import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import java.util.HashMap;
import java.util.Map;

@Slf4j
@RestController
public class ReportController {

    /**
     * Store view count per ad-session
     */
    private final Map<String, Integer> viewCounts = new HashMap<>();

    /**
     * API for user to notify whether ad was seen for a particular session
     *
     * @param sessionId session id
     */
    @GetMapping("/report/{sessionId}")
    public void getSession(@PathVariable final String sessionId) {
        log.info("Reporting ad-notify beat from session: {}", sessionId);
        viewCounts.put(sessionId, viewCounts.getOrDefault(sessionId, 0) + 1);
    }

    /**
     * API to get view counts for all the sessions for reporting purpose
     *
     * @return view count per session
     */
    @GetMapping("/report")
    public Map<String, Integer> getSession() {
        return viewCounts;
    }

}
