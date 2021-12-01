package com.example.metaversecodefestadserver.controllers.dto.response;

import lombok.Data;

@Data
public class AdReportDTO {

    /**
     * Session Id
     */
    private final String sessionId;

    /**
     * View count for this session
     */
    private Integer viewCount;

    /**
     * Avg. view time for this session
     */
    private Float avgViewTime;


    public AdReportDTO(final String sessionId) {
        this.sessionId = sessionId;
        this.viewCount = 0;
        this.avgViewTime = 0f;
    }

    public void addViewInfo(final float deltaTime) {
        this.viewCount++;
        this.avgViewTime += deltaTime;
    }

}
